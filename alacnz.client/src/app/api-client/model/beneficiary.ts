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


export interface Beneficiary { 
    beneficiaryId?: number;
    name?: string | null;
    age?: number;
    category?: string | null;
    nationality?: string | null;
    caseId?: number;
    'case'?: Case;
}

