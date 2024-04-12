﻿namespace Practice_3.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<EProgram> EPrograms { get; set; } = [];
    }
}
