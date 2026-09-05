import { Routes } from '@angular/router';
import { AuthLayout } from './Layouts/auth-layout/auth-layout';
import { MainLayout } from './Layouts/main-layout/main-layout';
import { NotFound } from './Features/_Common/not-found/not-found';
import { authGuard } from './Core/Guards/AuthGuard/auth-guard';
import { homeGuard } from './Core/Guards/HomeGuard/home-guard';

export const routes: Routes = [
    {
        path: "", component: AuthLayout, children: [
            { path: "", redirectTo: "Login", pathMatch: "full" },
            { path: "Login", loadComponent: ()=> import("./Features/AuthModule/Login/Login.component").then(c => c.LoginComponent)},
            { path: "Register", loadComponent: ()=> import("./Features/AuthModule/Register/Register.component").then(c => c.RegisterComponent)},
        ],
        canActivate: [authGuard]
    },
    {
        path: "", component: MainLayout, children: [
            { path: "", redirectTo: "Rooms", pathMatch: "full" },
            { path: "Rooms", loadComponent: () => import("./Features/RoomModule/Rooms/Rooms.component").then(c => c.RoomsComponent)},
            { path: "Reports", loadComponent: () => import("./Features/ReportsModule/Reports/Reports.component").then(c => c.ReportsComponent)},
            { path: "Reservations", loadComponent: () => import("./Features/ReservationModule/Reservations/Reservations.component").then(c => c.ReservationsComponent)},
            { path: "Users", loadComponent: () => import("./Features/AuthModule/Users/Users.component").then(c => c.UsersComponent)}
        ],
        canActivate: [homeGuard]
    },
    { path: "**", component: NotFound },
];
