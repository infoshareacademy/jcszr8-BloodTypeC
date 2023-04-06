﻿using BloodTypeC.DAL.Models;

namespace BloodTypeC.DAL.Repository
{
    public interface IRepository
    {
        public List<Beer> GetAll();
        public Beer Get(string id);
        public void Insert(Beer entity);
        public void Delete(Beer entity);
        public void Update(Beer entity);
    }
}
