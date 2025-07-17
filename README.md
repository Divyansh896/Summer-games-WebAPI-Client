# 🏅 Summer Games WebAPI Client

**Summer Games WebAPI Client** is a Windows-based application developed using **XAML and C#**, built to manage and visualize athletes, sports, and contingents in a structured sports event scenario. It connects to a RESTful WebAPI hosted on **Azure**, enabling full CRUD functionality and dynamic data display through an intuitive desktop UI.

> 🧑‍💻 **Individual Project**  
> 🗓️ **Development Period**: January 2025 – February 2025  
> 🖥️ **Tech**: UWP (XAML + C#) frontend + ASP.NET WebAPI backend (Swagger + SQLite)

---

## 📌 Project Overview

This application provides a front-end client that interacts with a **public REST API** hosted on Azure:

## 📢 Important Note

> ⚠️ **API Availability:**  
> The hosted API on Azure (`https://ddivyansh5projectwebapi.azurewebsites.net/`) may not always be online due to free-tier hosting limitations.  
>  
> 👉 **If the online API is not responsive**, please follow the steps below to run the API **locally** on your machine.

### 🔄 Running the API Locally

1. **Clone the Backend API Project**
   ```bash
   git clone https://github.com/Divyansh896/Summer-Games-API.git
   cd Summer-Games-API
   
🌐 **API Endpoint:** `https://ddivyansh5projectwebapi.azurewebsites.net/`

The system allows you to:

- Manage **Athletes**
- Organize **Sports**
- Track **Contingents** (countries or teams)
- Assign athletes to both sports and contingents
- View structured data on a dashboard
- Display dialogs for user actions and confirmations

---

## ✨ Core Features

### 🧍 Athlete Management
- Create, read, update, and delete athlete records
- Assign sports and contingents to each athlete
- Display a meaningful list of athletes in the dashboard

### 🏆 Sports Module
- Maintain a list of all sporting events
- Associate athletes with relevant sports

### 🌍 Contingents Module
- Manage teams or countries
- Connect athletes to specific contingents

### 📊 Dashboard UI (XAML)
- Built using XAML Pages for a clean, native Windows experience
- Group athletes by sport or contingent
- Dialogs for confirmation and error handling using `ContentDialog`

### 🔗 Web API Integration
- Fully connected to a custom-built RESTful API
- Uses `HttpClient` to perform all backend communication
- API exception handling with meaningful user messages

---

## 🔧 Tech Stack

### 🖥️ Frontend (UWP or WinUI Desktop App)
- **XAML Pages** for user interface
- **C#** code-behind for logic and API interaction
- **HttpClient** for REST communication
- **Newtonsoft.Json** for JSON serialization/deserialization
- **ContentDialog** for interactive modals

### 🌐 Backend (Web API)
- **ASP.NET Core Web API**
- **Swagger** for API documentation and testing
- **SQLite** for lightweight data storage
- **Hosted on Azure**

---

## 🧠 Notable Code Snippet: API Utility

The `Jeeves` utility class centralizes error handling and API interaction patterns:

```csharp
public static Uri DBUri = new Uri("https://ddivyansh5projectwebapi.azurewebsites.net/");
