import { singleton } from 'tsyringe';
import { PageViewModel } from 'XtiShared/PageViewModel';
import { AppListPanelViewModel } from './AppListPanelViewModel';
import * as template from './MainPage.html';

@singleton()
export class MainPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }

    readonly appListPanel = new AppListPanelViewModel();
}