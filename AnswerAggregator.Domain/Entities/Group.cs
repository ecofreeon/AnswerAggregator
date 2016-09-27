﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnswerAggregator.Domain.Entities
{
    public class Group : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<UserProfile> Students { get; set; }

        public virtual ICollection<GroupSubject> GroupSubjects { get; set; }
    }
}
