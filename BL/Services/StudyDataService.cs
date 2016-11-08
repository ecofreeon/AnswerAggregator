﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories.Interfaces;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class StudyDataService : ServiceBase, IStudyDataService
    {
        protected readonly IRepository<University> Universities;
        protected readonly IRepository<Institute> Institutes;
        protected readonly IRepository<Group> Groups; 

        public StudyDataService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Universities = UnitOfWork.Universities;
            Institutes = UnitOfWork.Institutes;
            Groups = UnitOfWork.Groups;
        }

        public async Task<IEnumerable<string>> GetUniversities()
        {
            var universities = await Universities.GetAll();
            var list = universities.Select(t => t.Name);
            return list;
        }

        public async Task<IEnumerable<string>> GetInstitutes(string universityName)
        {
            if (string.IsNullOrWhiteSpace(universityName))
                throw new ArgumentNullException("universityName", "Не задано название университета");

            return (await Institutes.GetList(t => t.University.Name == universityName)).Select(t => t.Name);
        }

        public async Task<IEnumerable<string>> GetGroups(string instituteName, int course)
        {
            if (string.IsNullOrWhiteSpace(instituteName))
                throw new ArgumentNullException("instituteName", "Не задано название института");

            int yearOfEntering = DateTime.Now.Year - course + 1;

            return (await Groups.GetList(
                t => t.Institute.Name == instituteName && t.DateOfEntering.Year == yearOfEntering))
                .Select(t => t.Name);
        }
    }
}