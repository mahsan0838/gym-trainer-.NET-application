# Flex Trainer --- Gym & Fitness Management System (C# / .NET + SQL Server)

## Description

Flex Trainer is a desktop application built in C# (.NET) with a SQL
Server backend. It is designed to manage gym operations: member
registration, workout planning, diet planning, trainer scheduling,
gym-owner and admin interfaces, and detailed reporting.

## Table of Contents

1.  [Features](#features)
2.  [System Requirements](#system-requirements)
3.  [Installation & Setup](#installation--setup)
4.  [Usage](#usage)
5.  [Database Schema & Data](#database-schema--data)
6.  [Project Structure](#project-structure)
7.  [Reports](#reports)
8.  [Contributing & License](#contributing--license)

## Features

### Member Interface

-   Registration & login
-   Create and manage workout plans (exercises, sets, reps, machines)
-   Browse existing workout plans (own, shared, trainer-created) with
    filters (goal, schedule, experience)
-   Create and manage diet plans (meals with nutritional info and
    allergens)
-   Browse/select existing diet plans (own, shared, trainer-designed)
    with filters (diet type, goal, nutrition, trainer)
-   Book personal training sessions with trainers
-   Provide feedback/rating for trainers

### Trainer Interface

-   Registration (gym selection → owner approval) & login
-   Manage appointments (schedule, reschedule, cancel)
-   Create custom workout and diet plans for clients
-   View reports on plans & clients with filtering
-   View aggregated feedback and ratings (global and per-gym)

### Gym Owner Interface

-   Registration (requires admin approval) & login
-   View detailed member reports (membership, activity)
-   View trainer reports (performance, clients, ratings)
-   Add new trainers, manage trainer accounts
-   Deactivate or remove member/trainer accounts

### Admin Interface

-   Secure login
-   Approve gym owner registrations
-   Approve/reject gyms or revoke gyms from the network
-   View performance reports across all gyms

### Reporting Module

Implements a set of predefined reports including but not limited to: -
Member-trainer-gym relationships - Diet and workout plan usage
analysis - Machine usage statistics per gym per day - Nutrition-based
diet plan filters - Membership growth and gym comparison over time

## System Requirements

-   Visual Studio 2019 or later (.NET 8.0 or compatible)
-   SQL Server
-   Windows (tested environment)

## Installation & Setup

``` bash
git clone https://github.com/mahsan0838/gym-trainer-.NET-application.git
cd gym-trainer-.NET-application/DB_project_milestone_2
open DB_project_milestone_2.sln with Visual Studio
```

Restore NuGet packages if required.

Configure connection string in application settings to point to your SQL
Server instance.

Use provided SQL scripts to create database schema and populate
seed/sample data (populate Users, Members, Gyms, Trainers etc.).

Build the solution and run the application.

## Usage

Use the login or registration forms to create accounts for Admin / Gym
Owner / Trainer / Member.

Depending on role, access respective interface.

For members: create workout + diet plans, book sessions, view feedback.

For trainers: manage clients, create plans, view reports.

For gym owners: oversee gyms, manage staff, view reports.

Admin handles gym approvals, overall system oversight.

## Database Schema & Data

Database is normalized.

Includes tables for Users, Members, Trainers, Gyms, Workouts, DietPlans,
Appointments, Feedback, etc.

Sample data seeded: at least 50 users/members and sufficient data across
other tables to support reports.

ERD / schema diagram to be included separately (if available).

## Project Structure

    /DB_project_milestone_2
    │  DB_project_milestone_2.sln
    │  [C# source folders/files]
    │  /Resources  — images/assets
    │  /bin / /obj — build output & dependencies
    │  [Other files]

## Reports

The application implements built-in queries and UI for generating these
reports (and more custom reports as per requirements).

Examples: - Members of a gym under a specific trainer - Members
following a given diet plan - Clients using specific gym equipment on a
given day - Diet plans filtered by nutritional criteria (calories,
carbs) - Membership growth over time across gyms

## Contributing & License

Contributions are welcome. If you wish to contribute: fork the
repository, create a feature branch, implement your changes, and open a
pull request.

Please include appropriate documentation and testing for new features.
