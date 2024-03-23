#region Usings
global using BLL.Interfaces;
global using BLL.Services;
global using BLL.Enums;

global using DTOs.UserDtos;
global using DTOs.CategoryDtos;
global using DTOs.ColorDtos;

global using DataAccessLayer;
global using DataAccessLayer.Entities;
global using DataAccessLayer.Interfaces;
global using DataAccessLayer.Repositories;

global using System.Security.Claims;
global using System.Text;

global using Newtonsoft.Json;

global using Asp.Versioning;
global using AspNetCoreRateLimit;
global using StackExchange.Redis;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
#endregion