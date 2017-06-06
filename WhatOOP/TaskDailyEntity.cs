namespace WhatOOP
{
    /// <summary>
    ///  用户反馈实体
    /// </summary>
    public class TaskDailyEntity
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        public long AccountId { get; set; }

        public string T_mk { get; set; }

        public int vm_id { get; set; }
    }
}