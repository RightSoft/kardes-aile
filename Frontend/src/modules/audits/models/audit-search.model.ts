import { PagedSearchModel } from "../../../app/models/shared/paged-search.model";
import { AuditTypesEnum } from "../enums/audit-types.enum";

export class AuditSearchModel extends PagedSearchModel {
  public filter?: string;
  public type?: AuditTypesEnum;
  public start?: string;
  public end?: string;
}
