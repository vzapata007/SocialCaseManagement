/**
 * alacnz.server
 *
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { Case } from './case';


export interface Service { 
    serviceId?: number;
    serviceType?: string | null;
    description?: string | null;
    cases?: Array<Case> | null;
}

