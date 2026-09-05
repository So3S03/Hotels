import { isPlatformBrowser, NgClass } from '@angular/common';
import { Component, inject, OnInit, PLATFORM_ID, signal, WritableSignal } from '@angular/core';
import { IModule } from '../../Core/Interfaces/_Common/IModule';
import { moduleArray } from '../../Core/_Common/modules.nav';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { LogOut, LucideAngularModule, LucideIconData, Menu } from "lucide-angular";
import { IJwtDecodedObject } from '../../Core/Interfaces/_Common/IJwtDecodedObject';

@Component({
  imports: [NgClass, RouterLink, RouterLinkActive, LucideAngularModule, RouterOutlet],
  selector: 'app-main-layout',
  styleUrl: './main-layout.css',
  templateUrl: './main-layout.html',
})
export class MainLayout implements OnInit{
  //DI Container
  private readonly _platform_key = inject(PLATFORM_ID);
  private readonly _Router: Router = inject(Router);

  //Common Vars
  isSideBarOpen: WritableSignal<boolean> = signal(true);
  moduleData: WritableSignal<IModule[]> = signal(moduleArray);
  activeModule: WritableSignal<string> = signal(`${this.moduleData()[0].Title} Module`);
  icons = {
    LogOut,
    Menu
  };
  userData: IJwtDecodedObject | null = null;

  //Logic
  ngOnInit(): void {
    this.extractUserData();
  }

  collapseSidebar(): void
  {
    this.isSideBarOpen.update(u => !u)
  }
  logOut(): void
  {
    if(isPlatformBrowser(this._platform_key)) {
      localStorage.removeItem("token");
      localStorage.removeItem("userData");
      this._Router.navigate(["/Login"])
    }
  }

  private extractUserData(): void
  {
    if(isPlatformBrowser(this._platform_key))
    {
      const data = localStorage.getItem("userData");
      if(data != null) this.userData = JSON.parse(data);
    }
  }
}
