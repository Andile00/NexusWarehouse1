<p align="center">
  <h1 align="center">NexusWarehouse – Smart Warehouse & Logistics Hub</h1>
</p>

<p align="center">

  <img src="https://img.shields.io/badge/Language-C%23-blue" alt="Language">
  <img src="https://img.shields.io/badge/Platform-Console-lightgrey" alt="Platform">
</p>

---

## Table of Contents
- [System Overview](#system-overview)
- [Key Features](#key-features)
- [Running the Application](#running-the-application)
- [Key Design Decisions](#key-design-decisions)
- [Multithreading and Events](#multithreading-and-events)

---

## System Overview

**NexusWarehouse** simulates a real-time warehouse management system where users manage the inventory of perishable and hazardous cargo through a console menu. The system enforces capacity limits, temperature monitoring for perishables, and safety clearances for hazardous items, and triggers alerts when violations occur. A background task independently monitors temperature conditions, while users interact with a menu to add, relocate, dispatch, and remove cargo items.

---

## Key Features

### Dynamic Inventory Management
Add, modify (relocate), and remove cargo items with real-time weight tracking and warehouse capacity enforcement (`1000kg` limit). Each operation updates the system state and prevents over-capacity additions through exception handling.

### Multi-Type Cargo System
Two cargo types with domain-specific constraints: Perishable cargo requires a temperature ≤ 5°C for dispatch; Hazardous cargo requires safety clearance. Violations prevent dispatch and trigger exceptions. This ensures realistic operational rules for different cargo types.

### Real-Time Monitoring & Alerts
An independent background task continuously monitors perishable cargo temperatures and fires alerts when unsafe conditions are detected, without blocking user interaction. The system runs background monitoring asynchronously using `Tasks` and threading.

### Event-Driven Architecture
System events (capacity exceeded, temperature alerts) decouple operations from notifications, allowing the console UI and file logging to subscribe independently to warehouse events. Publishers and subscribers communicate through custom delegates and events.

### Persistent Operation Logging
An audit trail of all warehouse actions (additions, removals, alerts) is automatically logged to `warehouse_log.txt` for compliance and troubleshooting. All system events are recorded with timestamps for traceability.

---

## Running the Application

### Using Visual Studio

1. **Open Visual Studio**
2. **File → Open → Project/Solution** and select `NexusWarehouse.sln`
3. **Build** (`Ctrl+Shift+B`)
4. **Run** (`Ctrl+F5` or press the green Play button)
5. The console window opens with the Warehouse Management Hub menu

### From Command Line

```bash
cd NexusWarehouse
csc /target:exe Program.cs Cargo.cs PerishableCargo.cs HazardousCargo.cs Interfaces.cs WarehouseManager.cs WarehouseExceptions.cs WarehouseEvents.cs Helpers.cs
NexusWarehouse.exe 
```

## Key Design Decisions

### 1. Using an abstract base class for `Cargo`
The system manages different cargo types with different rules; however, they share similar properties. By defining these shared properties in the abstract class, `Cargo`, child classes inherit the properties while maintaining their own individual functionality. This keeps the code duplication-free and cleaner. 

### 2. Running temperature monitoring as a separate background task
Temperature monitoring was implemented as a background task as opposed to running on the main menu. This prevented it from blocking user input on the main thread, thereby preventing the console from becoming unresponsive. Running in the background simulates real-world functions, allows continuous real-time checks, and keeps the menu responsive and stable. 

### 3. Using delegates and Events for System alarts
Alerts are cross-cutting concerns and should not be hardwired into the core business logic. The system raises events in `WarehouseManager`, while the console and file logger subscribe and respond independently. This keeps the design modular, easier to maintain, and flexible for adding new alert handlers without changing the core logic.
	
## Multithreading and Events

### Multithreading:
The application uses `Task.Run()` in `StartBackgroundMonitoring()` to run temperature checks on a separate thread, while the main thread handles the console menu. This means monitoring runs asynchronously with user input. A lock (`inventory`) is used when reading shared cargo data, thereby helping prevent race conditions between menu operations and background checks. 

### Events:
The application uses a delegate `WarehouseAlertHandler` and two events: `OnCapacityAlert` and `OnTemperatureAlert`. `WarehouseManager` raises these events when key conditions occur, such as capacity overload or unsafe temperatures. Subscribers (console output and file logging) react to those alerts independently, which keeps core warehouse logic decoupled from notification behavior.