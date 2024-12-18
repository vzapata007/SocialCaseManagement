/**
 * alacnz.server
 *
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { SocialWorkTeam } from './socialWorkTeam';
import { Service } from './service';
import { Client } from './client';
import { Beneficiary } from './beneficiary';
import { Session } from './session';


export interface Case { 
    caseId?: number;
    registrationDate?: string;
    closureDate?: string | null;
    status?: string | null;
    city?: string | null;
    services?: string | null;
    referredBy?: string | null;
    referralReason?: string | null;
    activationFeasibility?: string | null;
    clientId?: number | null;
    client?: Client;
    socialWorkTeamId?: number | null;
    socialWorkTeam?: SocialWorkTeam;
    serviceId?: number | null;
    service?: Service;
    beneficiaries?: Array<Beneficiary> | null;
    sessions?: Array<Session> | null;
}

