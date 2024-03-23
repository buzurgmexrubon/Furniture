# FURNITURE API
![](https://img.shields.io/badge/DotNet-8-blue.svg)
![](https://img.shields.io/badge/License-MIT-orange.svg)

> ## About
> The _Furniture_ API is the backend connection between the client side and the [PostgreSQL](https://www.postgresql.org/) database. This API utilizes multiple models: Category, Color, Feedback, FeedbackBan, Image, Furniture, OtpModel and User.


## Structure
<img src="./structure_files.png" alt="Structure files" width="300"/>


## Packages
- BLL (Business Logic Layer)
	- Admin
		- BLL.csproj
			- ![](https://img.shields.io/badge/Messager.EskizUz-2.0.0-blue.svg)
			- ![](https://img.shields.io/badge/Microsoft.AspNetCore.Authentication.JwtBearer-8.0.3-orange.svg)
			- ![](https://img.shields.io/badge/Microsoft.AspNetCore.Http.Features-5.0.17-yellow.svg)
			- ![](https://img.shields.io/badge/Microsoft.AspNetCore.Identity.EntityFrameworkCore-8.0.3-purple.svg)
		- DTOs.csproj
			- ![](https://img.shields.io/badge/TimeZoneConverter-6.1.0-papayawhip.svg)
	- Mobile
		- MobileBLL.csproj
			- ![](https://img.shields.io/badge/Newtonsoft.Json-13.0.3-1e13fe.svg)
		- MobileDTOs.csproj
- DAL (Data Access Layer)
	- DataAccessLayer.csproj
		- ![](https://img.shields.io/badge/Microsoft.AspNetCore.Identity.EntityFrameworkCore-8.0.3-green.svg)
- PL (Project Layer)
	- API.csproj
		- ![](https://img.shields.io/badge/Asp.Versioning.Mvc-8.0.0-11eecc.svg)
		- ![](https://img.shields.io/badge/AspNetCoreRateLimit-5.0.0-e1aacc.svg)
		- ![](https://img.shields.io/badge/Microsoft.Extensions.Caching.StackExchangeRedis-8.0.3-f00f00.svg)
		- ![](https://img.shields.io/badge/Npgsql.EntityFrameworkCore.PostgreSQL-8.0.2-green.svg) 
		- ![](https://img.shields.io/badge/Serilog-3.1.1-00ee11.svg)
		- ![](https://img.shields.io/badge/Serilog.AspNetCore-8.0.1-yellow.svg)
		- ![](https://img.shields.io/badge/Swashbuckle.AspNetCore-6.5.0-red.svg)



## Catalogue of Routes
### Admin
| HTTP Method | URL Path |
|--|--|
| POST | /api/v{version}/Admin/create |
| PUT | /api/v{version}/Admin/update |
| GET | /api/v{version}/Admin/get-all |
| DELETE | /api/v{version}/Admin/{id} |
| PUT | /api/v{version}/Admin/activate/{userId} |
| PUT | /api/v{version}/Admin/reset-password/{userId} |

### Auth
| HTTP Method | URL Path |
|--|--|
| POST | /api/v{version}/Auth/login |
| POST | /api/v{version}/Auth/register |
| POST | /api/v{version}/Auth/send-otp |
| POST | /api/v{version}/Auth/verify-otp |
| PUT | /api/v{version}/Auth/logout |
| PUT | /api/v{version}/Auth/change-password |
| DELETE | /api/v{version}/Auth/delete |
| POST | /api/v{version}/Auth/profile/set-avatar |
| PUT | /api/v{version}/Auth/profile/change-avatar |
| DELETE | /api/v{version}/Auth/profile/delete-avatar/{userId} |
| GET | /api/v{version}/Auth/validate-token |
| PUT | /api/v{version}/Auth/update-profile |

### Category
| HTTP Method | URL Path |
|--|--|
| GET | /api/v{version}/Category/{lang}/all |
| GET | /api/v{version}/Category/{lang}/paged |
| GET | /api/v{version}/Category/{lang}/{id} |
| GET | /api/v{version}/Category/{id} |
| POST | /api/v{version}/Category/{lang} |
| PUT | /api/v{version}/Category/{lang} |
| DELETE | /api/v{version}/Category/delete/{id} |
| PATCH | /api/v{version}/Category/archive/{id} |
| PATCH | /api/v{version}/Category/unarchive/{id} |

### Color
| HTTP Method | URL Path |
|--|--|
| GET | /api/v{version}/Color/{lang}/all |
| GET | /api/v{version}/Color/{lang}/paged |
| GET | /api/v{version}/Color/{lang}/{id} |
| GET | /api/v{version}/Color/{id} |
| POST | /api/v{version}/Color/{lang} |
| PUT | /api/v{version}/Color/{lang} |
| DELETE | /api/v{version}/Color/delete/{id} |
| PATCH | /api/v{version}/Color/archive/{id} |
| PATCH | /api/v{version}/Color/unarchive/{id} |

### Furniture
| HTTP Method | URL Path |
|--|--|
| GET | /api/v{version}/Furniture/{lang}/all |
| GET | /api/v{version}/Furniture/{lang}/paged |
| GET | /api/v{version}/Furniture/{lang}/{id} |
| GET | /api/v{version}/Furniture/{id} |
| POST | /api/v{version}/Furniture/{lang} |
| PUT | /api/v{version}/Furniture/{lang} |
| DELETE | /api/v{version}/Furniture/delete/{id} |
| PATCH | /api/v{version}/Furniture/archive/{id} |
| PATCH | /api/v{version}/Furniture/unarchive/{id} |

### Image
| HTTP Method | URL Path |
|--|--|
| POST | /api/v{version}/Image |
| DELETE | /api/v{version}/Image |
| POST | /api/v{version}/Image/multiple |
| DELETE | /api/v{version}/Image/multiple |

### Mobile
| HTTP Method | URL Path |
|--|--|
| GET | /api/v{version}/Mobile/categories/{lang} |
| GET | /api/v{version}/Mobile/categories/{id}/{lang} |
| GET | /api/v{version}/Mobile/furnitures/{lang} |
| GET | /api/v{version}/Mobilefurnitures/{id}/{lang} |
| GET | /api/v{version}/Mobilefurnitures/feedbacks/{furnitureId} |
| GET | /api/v{version}/Mobile/feedbacks/{id} |
| POST | /api/v{version}/Mobile/feedbacks |
| POST | /api/v{version}/Mobile/feedbacks/ban |

### Users
| HTTP Method | URL Path |
|--|--|
| GET | /api/v{version}/Users/all |
| GET | /api/v{version}/Users/{id} |
| DELETE | /api/v{version}/Users/{id} |

## Entity Relationship Diagram
> ![Structure](./structure.png)
>> ###### [(Show in Web-site)](https://drawsql.app/teams/my-manager/diagrams/furniture)

## Contributing💡
If you want to contribute to this project and make it better with new ideas, your pull request is very welcomed.

If you have any question or issue, It may have bugs that i have missed. You can create <a href="https://github.com/buzruk/Furniture/pulls">Pull request</a>.

