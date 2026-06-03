using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.API.Models.DTO;
using POS.API.Services;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
            private readonly IUserService _userService;

            public UsersController(IUserService userService)
            {
                _userService = userService;
            }

          
        [HttpGet]
        [ProducesResponseType(typeof(APIResponse<IEnumerable<UserDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<IEnumerable<UserDTO>>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                return Ok(
                    APIResponse<IEnumerable<UserDTO>>
                    .Ok(users, "Users retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    APIResponse<object>.Error(
                        500,
                        "Error retrieving users",
                        ex.Message));
            }
        }

       
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(APIResponse<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<UserDTO>>> GetUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(
                        APIResponse<object>.BadRequest(
                            "Invalid User Id"));
                }

                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound(
                        APIResponse<object>.NotFound(
                            $"User with Id {id} not found"));
                }

                return Ok(
                    APIResponse<UserDTO>.Ok(
                        user,
                        "User retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    APIResponse<object>.Error(
                        500,
                        "Internal Server Error",
                        ex.Message));
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(APIResponse<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<UserDTO>>> CreateUser( CreateUserDTO createUserDTO)
        {
            try
            {
                if (createUserDTO == null)
                {
                    return BadRequest(
                        APIResponse<object>.BadRequest(
                            "User data is required"));
                }

                if (await _userService.IsEmailExistsAsync(
                    createUserDTO.Email))
                {
                    return Conflict(
                        APIResponse<object>.Conflict(
                            $"User with email '{createUserDTO.Email}' already exists"));
                }

                var user = await _userService
                    .CreateUserAsync(createUserDTO);

                //if (user == null)
                //{
                //    return BadRequest(
                //        APIResponse<object>.BadRequest(
                //            "User creation failed"));
                //}

                var response =
                    APIResponse<UserDTO>.CreatedAt(
                        user,
                        "User created successfully");

                return CreatedAtAction(
                    nameof(GetUser),
                    new { id = user.Id },
                    response);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    APIResponse<object>.Error(
                        500,
                        "An error occurred while creating user",
                        ex.Message));
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIResponse<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<UserDTO>>> UpdateUser(int id,UpdateUserDTO updateUserDTO)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(
                        APIResponse<object>.BadRequest(
                            "Invalid User Id"));
                }

                if (updateUserDTO == null)
                {
                    return BadRequest(
                        APIResponse<object>.BadRequest(
                            "User data is required"));
                }
                // Prevent assigning an email that already belongs
                // to another user while allowing the current user
                // to keep their existing email.
                if (await _userService.IsEmailExistsForAnotherUserAsync(id,updateUserDTO.Email))
                {
                    return Conflict(
                        APIResponse<object>.Conflict(
                            $"User with email '{updateUserDTO.Email}' already exists"));
                }
                var updatedUser =
                    await _userService.UpdateUserAsync(
                        id,
                        updateUserDTO);

                if (updatedUser == null)
                {
                    return NotFound(
                        APIResponse<object>.NotFound(
                            $"User with Id {id} not found"));
                }

                return Ok(
                    APIResponse<UserDTO>.Ok(
                        updatedUser,
                        "User updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    APIResponse<object>.Error(
                        500,
                        "An error occurred while updating user",
                        ex.Message));
            }
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(APIResponse<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<UserDTO>>> DeleteUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(
                        APIResponse<object>.BadRequest(
                            "Invalid User Id"));
                }

                var deletedUser =
                    await _userService.DeleteUserAsync(id);

                if (deletedUser == null)
                {
                    return NotFound(
                        APIResponse<object>.NotFound(
                            $"User with Id {id} not found"));
                }

                return Ok(
                    APIResponse<UserDTO>.Ok(
                        deletedUser,
                        "User deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    APIResponse<object>.Error(
                        500,
                        "An error occurred while deleting user",
                        ex.Message));
            }
        }
    }
    }

