﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Models
{
    public class RequirementModel
    {
        public Guid RequirementId { get; set; }

        public Guid PositionId { get; set; }

        public Guid SkillId { get; set; }

        public string Experience { get; set; }

        public string? Notes { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
