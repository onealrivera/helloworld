using System;

namespace HelloWorld.Entity
{
    /// <summary>
    /// Message
    /// </summary>
    public class Message : Interface.IEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id {get;set;}
        /// <summary>
        /// Data
        /// </summary>
        public string Data {get;set;}

        /// <summary>
        /// Message constructor
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="data">data</param>
        public Message(int id, string data)
        {
            Id = id;
            Data = data;
        }
    }
}
