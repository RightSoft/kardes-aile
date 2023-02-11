import AddPageTitle from './add-page-title.abstract.class';
abstract class BaseListComponent extends AddPageTitle {
  protected keyword: string;
  protected isChecked: boolean;
  constructor(pageTitle: string) {
    super(pageTitle);
  }
  onSearch(keyword: string) {
    this.keyword = keyword;
  }
  onCheckboxChange(isChecked: boolean) {
    this.isChecked = isChecked;
  }
}

export default BaseListComponent;
