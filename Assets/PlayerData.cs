using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public partial class PlayerData {

    public static readonly string playerDataFileName  = "SaveFile.sav";

    private static readonly ulong shortenAlgorithmX = 3429348925U;
    private static readonly ulong shortenAlgorithmM = 59100191385324U;

    public static readonly int MaxDisplayNameLength = 10; 
    public static readonly string backupSuffix = ".backup";
    public static readonly string defaultNamePrefix = "Player";
    private static PlayerData local;

    public static PlayerData Local
    {
        get
        {
            try
            {
                if(local == null)
                {
                    //Load from binary
                    local = PlayerData.Load();
                }
            }
            catch(CryptographicException ce1)
            {
                Debug.LogError(ce1);
                Debug.LogWarning("Possible old save data or corrupt save data found, trying to migrate.");
                try
                {
                    local = Migration();
                    local.Save(); //after the migration it should overwrite the old save immediately.
                    Debug.Log("Migration complete");
                }
                catch(CryptographicException ce2)
                {
                    Debug.LogError(ce2);
                    local = new PlayerData(); //you get an empty save if migration also throws crypto
                    Debug.Log("Could not migrate. Creating a new save file.");
                }
            }
            return local;
        }
    }

    public string FormattedShortPlayerId
    {
        get 
        { 
            if(string.IsNullOrEmpty(PlayerId))
            {
                return "???-???-???";
            }
            else
            {
                return
                ShortPlayerId.Substring(0,3) +
                "-" +
                ShortPlayerId.Substring(3,3) +
                "-" +
                ShortPlayerId.Substring(6,3)
                ; 
            }
        }
    }

    private const string keyMessage = "Protobuf please work";
    private static byte[] Key => Compute8Bytes(Encoding.ASCII.GetBytes(keyMessage));
    private static byte[] Compute8Bytes(byte[] input)
    {
        SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
        byte[] hash = sha1.ComputeHash(input);
        Array.Resize<byte>(ref hash, 8);
        return hash;
    }

    public string DisplayNameString
    {
        get 
        { 
            if(DisplayName == null || DisplayName == "")
            {
                return "???";
            }
            else
            {
                return DisplayName; 
            }
        }
        set
        {
            if(value.Length > 0 && value.Length <= MaxDisplayNameLength)
            {
                DisplayName = value;
                Save();
            }
        }
    }

    public bool IsInitialized
    {
        get
        {
            if (DisplayName == null || DisplayName == "" || PlayerId == null || PlayerId == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    /// <summary>
    /// Does not destroy your save file
    /// </summary>
    public void Initialize()
    {
        Debug.Log("Initializing");
        if(IsInitialized)
        {
            //If you want this to work again you have to call Reset()
            throw new Exception("Already initialized");
        }
        this.StartPlaying = DateTime.UtcNow.ToString("s");
        this.DisplayName = defaultNamePrefix + UnityEngine.Random.Range(0,9999).ToString("0000");
        bool isShortUserIdGood = false;
        while(isShortUserIdGood == false)
        {
            //GUID based user ID generation
            Guid guid = Guid.NewGuid();
            this.PlayerId = guid.ToString();
            this.PlayerIdHash = guid.GetHashCode();
            this.ShortPlayerId = PlayerDataUtility.ShortenGUID(guid,shortenAlgorithmX,shortenAlgorithmM);
            isShortUserIdGood = PlayerDataUtility.IsShortUserIdGood(FormattedShortPlayerId);
        }
        Initialize2();
        Debug.Log("Initialized with Name : " + DisplayName + " ID : " + PlayerId + " SID : " + FormattedShortPlayerId);
        Save();
    }

    private void Initialize2()
    {

    }

    public void Save()
    {
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        LastUpdated = DateTime.UtcNow.ToString("s");
        SaveAs(playerDataFileName);
    }

    public void Backup()
    {
        SaveAs(playerDataFileName + backupSuffix);
    }

    public void RestoreBackup()
    {
        PlayerDataUtility.ApplySaveFileFromPersistent("", playerDataFileName + backupSuffix);
    }

    private void SaveAs(string name)
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes"); //So that iOS don't complain about protobuf's JITing 
        Debug.Log("Saved : " + Application.persistentDataPath);
        using (FileStream file = File.Create(Application.persistentDataPath + "/" + name))
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Key;
            des.GenerateIV();

            file.Write(des.IV,0,8);
            //Debug.Log("Writing " + BitConverter.ToString(des.IV));

            using (var cryptoStream = new CryptoStream(file, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (Google.Protobuf.CodedOutputStream cos = new Google.Protobuf.CodedOutputStream(cryptoStream))
                {
                    local.WriteTo(cos);
                }
            }
        }
    }

    public static void LocalReload()
    {
        local = Load();
    }

    private static PlayerData Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + playerDataFileName))
        {
            using(FileStream fileStream = File.Open(Application.persistentDataPath + "/" + playerDataFileName, FileMode.Open))
            {
                PlayerData loaded = PlayerDataFromStream(fileStream);
                return loaded;
            }
        }
        else
        {
            return new PlayerData();
        }
    }

    private static PlayerData PlayerDataFromStream(Stream stream)
    {
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes"); //So that iOS don't complain about protobuf's JITing 

        //Try to get the IV..
        byte[] ivRead = new byte[8];
        stream.Read(ivRead,0,8);

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        des.Key = Key;
        des.IV = ivRead;

        PlayerData loadedData = new PlayerData();
        using (var cryptoStream = new CryptoStream(stream, des.CreateDecryptor(), CryptoStreamMode.Read))
        {
            using (Google.Protobuf.CodedInputStream cis = new Google.Protobuf.CodedInputStream(cryptoStream))
            {
                loadedData = PlayerData.Parser.ParseFrom(cis);
                return loadedData;
            }
        }
    }

    public static void MergeSave(PlayerData toMergeWith)
    {
        local = toMergeWith;
        PlayerData.Local.Save(); //you might not want auto-save on merge.
    }

    public static PlayerData Migration()
    {
        return new PlayerData();
    }

    /// <summary>
    /// For example getting a save restore as a JSON.
    /// </summary>
    public static PlayerData PlayerDataFromBase64(string base64String)
    {
        byte[] byteData = Convert.FromBase64String(base64String);
        using(MemoryStream memStream = new MemoryStream(byteData))
        {
            return PlayerDataFromStream(memStream);
        }
    }

    /// <summary>
    /// If you want to put the entire save in JSON this is useful
    /// </summary>
    public static string GeneratePlayerDataBase64()
    {
        PlayerData.Local.Save();
        if (File.Exists(Application.persistentDataPath + "/" + playerDataFileName))
        {
            return Convert.ToBase64String(File.ReadAllBytes(Application.persistentDataPath + "/" + playerDataFileName));
        }
        else
        {
            throw new Exception("Save file does not exist!");
        }
    }

    /// <summary>
    /// SUPER DESTRUCTIVE OPERATION please be careful!
    /// </summary>
    public static void Reset()
    {
        local = new PlayerData();
        local.Save();
    }


}
