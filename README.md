# FURNITURE API
![](https://img.shields.io/badge/DotNet-8-blue.svg)
![](https://img.shields.io/badge/License-MIT-orange.svg)

> ## About
> The _Furniture_ API is the backend connection between the client side and the [PostgreSQL](https://www.postgresql.org/) database. This API utilizes multiple models: Category, Color, Feedback, FeedbackBan, Image, Furniture, OtpModel and User.


## Structure
<img src="./structure_files.png" alt="Structure files" width="200"/>


## Packages
- BLL (Business Logic Layer)
-- Admin
--- BLL.csproj
---- ![](https://img.shields.io/badge/Messager.EskizUz-2.0.0-blue.svg)
---- ![](https://img.shields.io/badge/Microsoft.AspNetCore.Authentication.JwtBearer-8.0.3-orange.svg)
---- ![](https://img.shields.io/badge/Microsoft.AspNetCore.Http.Features-5.0.17-yellow.svg)
---- ![](https://img.shields.io/badge/Microsoft.AspNetCore.Identity.EntityFrameworkCore-8.0.3-purple.svg)
--- DTOs.csproj
---- ![](https://img.shields.io/badge/TimeZoneConverter-6.1.0-papayawhip.svg)
-- Mobile
--- MobileBLL.csproj
---- ![](https://img.shields.io/badge/Newtonsoft.Json-13.0.3-1e13fe.svg)
--- MobileDTOs.csproj
- DAL (Data Access Layer)
-- DataAccessLayer.csproj
--- ![](https://img.shields.io/badge/Microsoft.AspNetCore.Identity.EntityFrameworkCore-8.0.3-green.svg)
- PL (Project Layer)
-- API.csproj
--- ![](https://img.shields.io/badge/Asp.Versioning.Mvc-8.0.0-11eecc.svg)
--- ![](https://img.shields.io/badge/AspNetCoreRateLimit-5.0.0-e1aacc.svg)
--- ![](https://img.shields.io/badge/Microsoft.Extensions.Caching.StackExchangeRedis-8.0.3-f00f00.svg)
--- ![](https://img.shields.io/badge/Npgsql.EntityFrameworkCore.PostgreSQL-8.0.2-green.svg) 
--- ![](https://img.shields.io/badge/Serilog-3.1.1-00ee11.svg)
--- ![](https://img.shields.io/badge/Serilog.AspNetCore-8.0.1-yellow.svg)
--- ![](https://img.shields.io/badge/Swashbuckle.AspNetCore-6.5.0-red.svg)



## Catalogue of Routes
### Admin
>>> | HTTP Method | URL Path |
>>> |--|--|--|--|
>>> | POST | /api/v1/Admin/create |
>>> | PUT | /api/v1/Admin/update |
>>> | GET | /api/v1/Admin/get-all |
>>> | DELETE | /api/v1/Admin/{id} |
>>> | PUT | /api/v1/Admin/activate/{userId} |
>>> | PUT | /api/v1/Admin/reset-password/{userId} |

### Auth
>>> | HTTP Method | URL Path |
>>> |--|--|--|--|
>>> | POST | /api/v1/Auth/login |
>>> | POST | /api/v1/Auth/register |
>>> | POST | /api/v1/Auth/send-otp |
>>> | POST | /api/v1/Auth/verify-otp |
>>> | PUT | /api/v1/Auth/logout |
>>> | PUT | /api/v1/Auth/change-password |
>>> | DELETE | /api/v1/Auth/delete |
>>> | POST | /api/v1/Auth/profile/set-avatar |
>>> | PUT | /api/v1/Auth/profile/change-avatar |
>>> | DELETE | /api/v1/Auth/profile/delete-avatar/{userId} |
>>> | GET | /api/v1/Auth/validate-token |
>>> | PUT | /api/v1/Auth/update-profile |

### Category
>>> | HTTP Method | URL Path |
>>> |--|--|--|--|
>>> | GET | /api/v1/Category/{lang}/all |
>>> | GET | /api/v1/Category/{lang}/paged |
>>> | GET | /api/v1/Category/{lang}/{id} |
>>> | GET | /api/v1/Category/{id} |
>>> | POST | /api/v1/Category/{lang} |
>>> | PUT | /api/v1/Category/{lang} |
>>> | DELETE | /api/v1/Category/delete/{id} |
>>> | PATCH | /api/v1/Category/archive/{id} |
>>> | PATCH | /api/v1/Category/unarchive/{id} |

### Color
>>> | HTTP Method | URL Path |
>>> |--|--|--|--|
>>> | GET | /api/v1/Color/{lang}/all |
>>> | GET | /api/v1/Color/{lang}/paged |
>>> | GET | /api/v1/Color/{lang}/{id} |
>>> | GET | /api/v1/Color/{id} |
>>> | POST | /api/v1/Color/{lang} |
>>> | PUT | /api/v1/Color/{lang} |
>>> | DELETE | /api/v1/Color/delete/{id} |
>>> | PATCH | /api/v1/Color/archive/{id} |
>>> | PATCH | /api/v1/Color/unarchive/{id} |

### Furniture
>>> | HTTP Method | URL Path |
>>> |--|--|--|--|
>>> | GET | /api/v1/Furniture/{lang}/all |
>>> | GET | /api/v1/Furniture/{lang}/paged |
>>> | GET | /api/v1/Furniture/{lang}/{id} |
>>> | GET | /api/v1/Furniture/{id} |
>>> | POST | /api/v1/Furniture/{lang} |
>>> | PUT | /api/v1/Furniture/{lang} |
>>> | DELETE | /api/v1/Furniture/delete/{id} |
>>> | PATCH | /api/v1/Furniture/archive/{id} |
>>> | PATCH | /api/v1/Furniture/unarchive/{id} |

### Image
>>> | HTTP Method | URL Path |
>>> |--|--|--|--|
>>> | POST | /api/v1/Image |
>>> | DELETE | /api/v1/Image |
>>> | POST | /api/v1/Image/multiple |
>>> | DELETE | /api/v1/Image/multiple |

### Mobile
>>> | HTTP Method | URL Path |
>>> |--|--|--|--|
>>> | GET | /api/v1/Mobile/categories/{lang} |
>>> | GET | /api/v1/Mobile/categories/{id}/{lang} |
>>> | GET | /api/v1/Mobile/furnitures/{lang} |
>>> | GET | /api/v1/Mobilefurnitures/{id}/{lang} |
>>> | GET | /api/v1/Mobilefurnitures/feedbacks/{furnitureId} |
>>> | GET | /api/v1/Mobile/feedbacks/{id} |
>>> | POST | /api/v1/Mobile/feedbacks |
>>> | POST | /api/v1/Mobile/feedbacks/ban |

### Users
>>> | HTTP Method | URL Path |
>>> |--|--|--|--|
>>> | GET | /api/v1/Users/all |
>>> | GET | /api/v1/Users/{id} |
>>> | DELETE | /api/v1/Users/{id} |

## Entity Relationship Diagram
> ![Structure](./structure.png)
>> ###### [(Show in Web-site)](https://drawsql.app/teams/my-manager/diagrams/furniture)

## Contributing💡
If you want to contribute to this project and make it better with new ideas, your pull request is very welcomed.

If you have any question or issue, It may have bugs that i have missed. You can create <a href="https://github.com/buzruk/Furniture/pulls">Pull request</a>.

