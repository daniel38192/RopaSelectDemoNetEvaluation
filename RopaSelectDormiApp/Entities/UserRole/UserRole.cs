using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RopaSelectDormiApp.Entities.UserRole;

using RopaSelectDormiApp.Entities.User;
using RopaSelectDormiApp.Entities.Role;

public class UserRole: IdentityUserRole<Guid>
{
}