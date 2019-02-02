using System;
using System.Linq;

using HelloWorld.Api.Controllers;
using HelloWorld.Entity;
using HelloWorld.Interface;
using HelloWorld.Store;

using Xunit;

namespace HelloWorld.Test.ApiTest
{
    public class MessageControllerTest
    {
        [Fact]
        public void Get_EmptySet_ReturnsZero()
        {
            using (var store = Factory.GetStore())
            {
                var controller = new MessageController(store);
                var result = controller.Get();
                Assert.True(result.Count() == 0);
            }
        }

        [Fact]
        public void GetById_MissingId_ReturnsNull()
        {
            using (var store = Factory.GetStore())
            {
                var controller = new MessageController(store);
                var result = controller.Get(100);
                Assert.True(result == null);
            }
        }

        [Theory]
        [InlineData(5)]
        public void Get_WithData_RetursCorrectCount(int entityCount)
        {
            using (var store = Factory.GetStore())
            {
                var controller = new MessageController(store);
                store.Save<Message>(GetEntities(entityCount));
                var result = controller.Get();
                Assert.True(result.Count() == entityCount);
            }
        }


        private static Message[] GetEntities(int entityCount)
        {
            var messages = new Message[entityCount];
            for(var i = 0; i < entityCount; i++)
            {
                messages[i] = new Message(i, "Hello World!");
            }
            return messages;
        }
    }
}
