using Hw14.Contracts;
using Hw14.Dto;
using Hw14.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.IO;
namespace Hw14.Repositories
{
    public class UserRepository : IUsreRepository
    {
        private List<VerificationDto> codes = new List<VerificationDto>();
        private string _verificationCodePath = "C:/Hw15/verificationCodes.txt";
        public UserRepository()
        {
            var directory = Path.GetDirectoryName(_verificationCodePath);
            if (Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(_verificationCodePath))
            {
                Directory.CreateDirectory(directory);
                File.WriteAllText(_verificationCodePath, "[]");
            }
        }
        public void GenerateAndSaveVerificationCode(int userId, string fullName, int verificationCode, DateTime expirationTime)
        {
            var data = File.ReadAllText(_verificationCodePath);
            var verificationDataList = JsonConvert.DeserializeObject<List<VerificationDto>>(data);
            if (verificationDataList == null)
            {
                verificationDataList = new List<VerificationDto>();
            }
            var verification = verificationDataList.FirstOrDefault(v => v.Id == userId);
            if (verification != null)
            {   
                verification.FullName = fullName;
                verification.VerificationCode = verificationCode;
                verification.DateVerificationCode = expirationTime;
            }
            else
            {
                var verificationDto = new VerificationDto
                {
                    Id = userId,
                    FullName = fullName,
                    VerificationCode = verificationCode,
                    DateVerificationCode = expirationTime
                };
                verificationDataList.Add(verificationDto);
            }
            string json = JsonConvert.SerializeObject(verificationDataList);
            File.WriteAllText(_verificationCodePath, json);
        }

        public VerificationDto GetVerificationDataById(int userId)
        {
            var data = File.ReadAllText(_verificationCodePath);
            var verificationCode = JsonConvert.DeserializeObject<List<VerificationDto>>(data);
            return verificationCode.FirstOrDefault(x => x.Id == userId);
        }
        public void SaveVerificationData(VerificationDto verificationDto)
        {
            var data = File.ReadAllText(_verificationCodePath);
            var verificationDataList = JsonConvert.DeserializeObject<List<VerificationDto>>(data);
            if (verificationDataList == null)
            {
                verificationDataList = new List<VerificationDto>();
            }
            var verification = verificationDataList.FirstOrDefault(v => v.Id == verificationDto.Id);
            if (verification != null)
            {  
                verification.FullName = verificationDto.FullName;
                verification.VerificationCode = verificationDto.VerificationCode;
                verification.DateVerificationCode = verificationDto.DateVerificationCode;
            }
     
            else
            {
                verificationDataList.Add(verificationDto);
            }
            var jsonData = JsonConvert.SerializeObject(verificationDataList);
            File.WriteAllText(_verificationCodePath, jsonData);
        }
    }

}

