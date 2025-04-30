# 🏥 Hospital Management System (HMS)

## 📘 Overview
The **Hospital Management System (HMS)** is a comprehensive, modular application developed to enhance the efficiency of hospital operations through digital automation. Built using **.NET 9** and **C# 13.0**, the system provides secure, role-based access to manage users, roles, patients, doctors, appointments, billing and medical records through a modern, API-first architecture.



## 🚀 Core Features

### 🔐 Authentication & RBAC
- Secure JWT-based login and registration  
- Role-Based Access Control (RBAC) to enforce permissions based on user roles  

### 🛡️ Validation & Error Handling
- Strong input validation  
- Centralized error handling using custom exceptions  

### 👤 User Management
- Full CRUD operations for user accounts  
- Role assignment and access control  

### 🧑‍⚕️ Patient & Doctor Management
- Manage patient profiles and medical histories  
- Manage doctor information, specialties and availability  

### 📅 Appointment Scheduling
- Create, update, and cancel appointments  
- Filter appointments by doctor, patient and date  

### 💰 Billing System
- Generate and manage bills  
- Track payments and outstanding balances  

### 📄 Medical Records
- Create, view, update and audit medical records

## 🛠️ Tools & Technologies
- **.NET 9 & C# 13.0**  
- **Entity Framework Core** (ORM)  
- **Logging Framework** (log4net)
- **JWT** for authentication  
- **LINQ** for efficient database queries  
- **Swagger** for API documentation  
- **Postman** for testing  
- Visual Studio 2022  



## 📂 Project Structure
```
HMS/
├── HMS.API                        → API controllers & Startup configuration
├── HMS.API.Services              → Business logic & validation services
├── HMS.DAL                       → Data access layer (DbContext, Repositories)
├── HMS.API.Abstraction           → Shared entities, interfaces, enums, exceptions
├── HMS.DAL.Models                → EF Core models and configurations
├── HMS.DAL.DataAccess.Utilities  → Reusable helpers and extensions
```




## 🧩 Project Layers Overview

### 1. `HMS.API`
Handles incoming HTTP requests and routes them through appropriate controllers.

- Feature-specific controllers
- API models (request/response structures)  
- `Startup.cs` for dependency injection and middleware setup  

### 2. `HMS.API.Services`
Contains business logic and validation.

- Core service implementations
- Data validation  
- Service interfaces  

### 3. `HMS.DAL`
Responsible for all database operations via EF Core.

- ApplicationDbContext  
- Repositories and Data Managers  

### 4. `HMS.API.Abstraction`
Shared contracts, exceptions, and enums.

- Enums
- Custom exceptions
- Interfaces and shared DTOs  

### 5. `HMS.DAL.Models`
Database entities and relationships.

- EF Core model definitions  
- Fluent API configuration  

### 6. `HMS.DAL.DataAccess.Utilities`
- Helper functions and extensions for data access operations  



## 🛡️ Filters

- **Authentication Filter**: Validates JWTs and secures endpoints  
- **Base Filter**: Common logic for validation, authorization, and logging  



## 🧾 Data Managers Overview

### 📌 UserDataManager
- `GetAllUsers`, `GetUserById`, `UpdateUser`, `DeleteUser`  ,
`GetUserByEmail`, `GetUser` (for login), `RegisterNewUser`  

### 📌 PatientDataManager
- `GetAllPatients`, `GetPatientById`, `UpdatePatient`, `DeletePatient` , `GetPatientByEmail`, `RegisterNewPatient`  

### 📌 DoctorDataManager
- `GetAllDoctors`, `GetDoctorById`, `UpdateDoctor`, `DeleteDoctor` ,  `GetDoctorByEmail`, `RegisterNewDoctor`  

### 📌 AppointmentDataManager
- `GetAllAppointments`, `GetAppointmentById`, `CreateAppointment`, `UpdateAppointment`, `DeleteAppointment`  
- Filtering: by patient name/date, doctor name/date  

### 📌 BillingDataManager
- `GetAllBills`, `GetBillById`, `CreateBill`, `UpdateBill`, `DeleteBill`  

### 📌 MedicalRecordDataManager
- `GetAllMedicalRecords`, `GetMedicalRecordById`, `CreateMedicalRecord`, `UpdateMedicalRecord`, `DeleteMedicalRecord`  



## 👥 Role-Based Access Control (RBAC)

| **Role** | **Features** |
|----------|--------------|
| **Admin** | Full access to all system features including user, doctor, patient, appointment, billing, and medical record management |
| **Doctor** | Manage profile, view/update assigned patients, access medical records, and schedule appointments |
| **Patient** | View/update profile, book appointments, view medical history and billing details |


## 📸 Screenshots
![WhatsApp Image 2025-04-15 at 05 11 13_35efe52b](https://github.com/user-attachments/assets/25b56edb-a357-41a9-848a-2b596a802ba8)  
![WhatsApp Image 2025-04-15 at 05 11 46_c555655d](https://github.com/user-attachments/assets/bca2171b-8b59-4ca6-89b2-33e35a973aeb)  
![WhatsApp Image 2025-04-15 at 05 12 06_f1c19558](https://github.com/user-attachments/assets/06d26e6a-d423-4df2-9a37-41cdded8327f)  
![image](https://github.com/user-attachments/assets/474276e2-9951-4783-b6cc-11fd771022c2)  
![image](https://github.com/user-attachments/assets/937dbb32-102a-471f-86a9-80ff73adf2ee)  
![image](https://github.com/user-attachments/assets/ccdd1505-969e-4646-a9d5-3b98a196b5ef)








