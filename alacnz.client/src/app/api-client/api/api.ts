export * from './alert.service';
import { AlertService } from './alert.service';
export * from './beneficiary.service';
import { BeneficiaryService } from './beneficiary.service';
export * from './case.service';
import { CaseService } from './case.service';
export * from './client.service';
import { ClientService } from './client.service';
export * from './service.service';
import { ServiceService } from './service.service';
export * from './session.service';
import { SessionService } from './session.service';
export * from './socialWorkTeam.service';
import { SocialWorkTeamService } from './socialWorkTeam.service';
export const APIS = [AlertService, BeneficiaryService, CaseService, ClientService, ServiceService, SessionService, SocialWorkTeamService];
