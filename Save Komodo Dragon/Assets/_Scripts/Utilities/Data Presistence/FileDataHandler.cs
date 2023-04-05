using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler{
    public FileDataHandler(string dir, string fileName){
        this.dataDirPath = dir;
        this.dataFileName = fileName;
    }
    private string dataDirPath, dataFileName;
    public GameData Load(){
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath)){
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)){
                    using (StreamReader reader = new StreamReader(stream)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }else{
            return new GameData();
        }
        return loadedData;
    }
    public void Save(GameData gameData){
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try{
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(gameData, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create)){
                using(StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }catch(Exception e){
            Debug.LogError(e);
        }
    }
    
}