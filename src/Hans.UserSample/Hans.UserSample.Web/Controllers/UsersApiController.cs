using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hans.UserSample.Core.Entities;
using Hans.UserSample.Core.Interfaces;
using Hans.UserSample.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hans.UserSample.Web.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IRepository<User> repository;

        public UsersApiController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        // GET
        // api/v1/users/all
        /// <summary>
        /// Retrieves users
        /// </summary>
        /// <returns>A response with user list</returns>
        /// <response code="200">Returns the user list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Users")]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var users = await repository.FindAllAsync();

            return Ok(users);
        }

        // GET
        // api/v1/users/5
        /// <summary>
        /// Retrieves a user by ID
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>A response with a user</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">If users is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("users/{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404, Type = typeof(User))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await repository.FindOneByAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // DELETE
        // api/v1/users/5
        /// <summary>
        /// Deletes an existing user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>A response as delete user result</returns>
        /// <response code="200">If user deleted successfully</response>
        /// <response code="404">If users is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("StockItem/{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404, Type = typeof(User))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await repository.FindOneByAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            repository.Delete(user);

            return Ok();
        }

        // POST
        // api/v1/user
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="model">Request model</param>
        /// <returns>A response with new user</returns>
        /// <response code="201">A response as creation of user</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("User")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(User))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody]User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await repository.Save(model);

            return Ok();

        }

        // PUT
        // api/v1/users/5
        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="model">Request model</param>
        /// <returns>A response as update user result</returns>
        /// <response code="200">If user updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="404">If user is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("StockItem/{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(User))]
        [ProducesResponseType(404, Type = typeof(User))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(Guid id, [FromBody]User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await repository.FindOneByAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            user.Username = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Role = model.Role;
            user.Email = model.Email;
            user.Phone = model.Phone;

            await repository.Update(user);

            return Ok();
        }
    }
}