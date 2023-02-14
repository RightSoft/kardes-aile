export class PagedResultModel<T>
{
    public totalCount: number;
    public list: T[];
    constructor() {
        this.list = [];
    }
}
