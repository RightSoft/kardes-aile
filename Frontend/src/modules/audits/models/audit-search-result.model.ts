import { AuditTypesEnum } from "../enums/audit-types.enum";

export interface AuditSearchResultModel {
  id: string;
  type: AuditTypesEnum;
  action: string;
  createdBy: string;
  createdAt: string;
}
