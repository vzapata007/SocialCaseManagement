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


export interface Alert { 
    id?: number;
    casoId?: number;
    caso?: Case;
    fechaAlerta?: string;
    tipoAlerta?: string | null;
    descripcion?: string | null;
}
