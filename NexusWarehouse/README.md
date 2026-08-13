<p align="center">
  <h1 align="center">NexusWarehouse – Smart Warehouse & Logistics Hub</h1>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Status-In_Development-orange" alt="Status">
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

**NexusWarehouse** simulates a real-time warehouse management system where users manage inventory of perishable and hazardous cargo through a console menu. The system enforces capacity limits, temperature monitoring for perishables, and safety clearances for hazardous items—triggering alerts when violations occur. A background task independently monitors temperature conditions, while users interact with a menu to add, relocate, dispatch, and remove cargo items.

---

## Key Features

### Dynamic Inventory Management
Add, modify (relocate), and remove cargo items with real-time weight tracking and warehouse capacity enforcement (`1000kg` limit). Each operation updates the system state and prevents over-capacity additions through exception handling.

### Multi-Type Cargo System
Two cargo types with domain-specific constraints: Perishable cargo requires temperature ≤ 5°C for dispatch; Hazardous cargo requires safety clearance—violations prevent dispatch and trigger exceptions. This ensures realistic operational rules for different cargo types.

### Real-Time Monitoring & Alerts
Independent background task continuously monitors perishable cargo temperatures and fires alerts when unsafe conditions detected, without blocking user interaction. The system runs background monitoring asynchronously using `Tasks` and `threading`.

### Event-Driven Architecture
System events (capacity exceeded, temperature alerts) decouple operations from notifications, allowing console UI and file logging to subscribe independently to warehouse events. Publishers and subscribers communicate through custom delegates and events.

### Persistent Operation Logging
Audit trail of all warehouse actions (additions, removals, alerts) automatically logged to `warehouse_log.txt` for compliance and troubleshooting. All system events are recorded with timestamps for traceability.

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
To be completed

## Multithreading and Events
To be completed