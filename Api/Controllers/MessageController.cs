using System;
using System.Collections.Generic;

using HelloWorld.Entity;
using HelloWorld.Interface;

using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Api.Controllers
{
    /// <summary>
    /// Message Controller
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private IStore _store;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="storage">Storage implementation</param>
        public MessageController(IStore storage)
        {
            _store = storage;
        }

        /// <summary>
        /// Gets all messages
        /// </summary>
        /// <returns>Message collection</returns>
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Message>))]
        [ProducesResponseType(204)]
        public IEnumerable<Message> Get()
        {
            return _store.Get<Message>();
        }

        /// <summary>
        /// Get message instance that matches the id
        /// </summary>
        /// <param name="id">Message id</param>
        /// <returns>Message instance</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type=typeof(Message))]
        [ProducesResponseType(204)]
        public Message Get(int id)
        {
            return _store.Get<Message>(id);
        }

        /// <summary>
        /// Inserts a new message into the store
        /// </summary>
        /// <param name="value">Message instance</param>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void Post([FromBody]Message value)
        {
            _store.Save(value);
        }

        /// <summary>
        /// Updates an existing message in the store
        /// </summary>
        /// <param name="id">Message Id</param>
        /// <param name="value">Message instance</param>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void Put(int id, [FromBody]Message value)
        {
            _store.Save(value);
        }

        /// <summary>
        /// Deletes an existing message from the store
        /// </summary>
        /// <param name="id">Message id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public void Delete(int id)
        {
            _store.Delete<Message>(id);
        }
    }
}
