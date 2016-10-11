﻿using System.Collections.Generic;

namespace BL.Enviroment
{
    public struct OperationResult
    {
        public bool Success { get; set; }

        public ICollection<string> Errors { get; set; }

        public OperationResult(bool success) : this()
        {
            Success = success;
        }
    }
}
