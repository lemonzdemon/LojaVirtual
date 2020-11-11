using BiaBraga.Business.Models;
using BiaBraga.Repository.Classes;
using System;
using System.IO;

namespace BiaBraga.Business.Classes
{
    public class GetConfigs
    {
        private readonly string FileConfig = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.FullName, "config.txt");
        private ModelConfig ModelConfig = new ModelConfig();

        public GetConfigs()
        {
            VerifyFileConfig();
        }

        private void VerifyFileConfig()
        {
            if (!File.Exists(FileConfig))
            {
                Config.Program.Main(new string[] { });
                EncriptModelConfig();
            }
            else
            {
                DecriptModelConfig();
            }
        }

        private void EncriptModelConfig()
        {
            string stringConfigs = ReadAllFileConfig();

            string[] configs = stringConfigs.Split("<!split!>");

            ModelConfig.ConnectionString = configs[0];
            ModelConfig.SecretKey = configs[1];

            if (string.IsNullOrEmpty(configs[2]))
            {
                configs[2] = DateTime.UtcNow.ToString("ddMMyyyyHHmmss");
            }

            Encript._key = configs[2];
            configs[0] = $"{Encript.Encrypt(configs[0])}<!split!>";
            configs[1] = $"{Encript.Encrypt(configs[1])}<!split!>";

            WriteAllFileConfig(string.Concat(configs));
        }

        private void DecriptModelConfig()
        {
            string stringConfigs = ReadAllFileConfig();

            string[] configs = stringConfigs.Split("<!split!>");
            Encript._key = configs[2];
            ModelConfig.ConnectionString = Encript.Decrypt(configs[0]);
            ModelConfig.SecretKey = Encript.Decrypt(configs[1]);
        }

        private string ReadAllFileConfig()
        {
            using StreamReader streamReader = new StreamReader(FileConfig);
            string result = streamReader.ReadToEnd();
            streamReader.Close();

            return result;
        }

        private void WriteAllFileConfig(string value)
        {
            using StreamWriter streamWriter = new StreamWriter(FileConfig);
            streamWriter.Write(value);
            streamWriter.Close();
        }
        

        /// <summary>
        /// Seleciona modelConfig que contem em arquivo config; se não, devolve configs padrao de codigo
        /// </summary>
        /// <returns></returns>
        public ModelConfig GetModelConfig()
        {
            return new ModelConfig
            {
                ConnectionString = string.IsNullOrEmpty(ModelConfig.ConnectionString) ?
                "server=localhost;userid=root;password=123456;database=biabraga" :
                ModelConfig.ConnectionString,

                SecretKey = string.IsNullOrEmpty(ModelConfig.SecretKey) ? "4F8B2A41D93D573E81D5C1937BAAF" : ModelConfig.SecretKey
            };
        }
    }
}
