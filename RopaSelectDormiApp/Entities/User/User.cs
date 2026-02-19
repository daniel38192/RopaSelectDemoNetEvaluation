using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RopaSelectDormiApp.Entities.User;

using RopaSelectDormiApp.Entities.UserRole;

public class User: IdentityUser<Guid>
{
}