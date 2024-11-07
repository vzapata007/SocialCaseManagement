import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CasosComponent } from './components/casos/casos.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { AlertasComponent } from './components/alertas/alertas.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'casos', component: CasosComponent },
  { path: 'clientes', component: ClientesComponent },
  { path: 'alertas', component: AlertasComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
