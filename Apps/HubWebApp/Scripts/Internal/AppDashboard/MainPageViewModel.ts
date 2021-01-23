import { singleton } from 'tsyringe';
import { PageViewModel } from 'XtiShared/PageViewModel';
import { PanelViewModel } from '../Panel/PanelViewModel';
import { AppDetailPanelViewModel } from './AppDetail/AppDetailPanelViewModel';
import * as template from './MainPage.html';
import { ResourceGroupPanelViewModel } from './ResourceGroup/ResourceGroupPanelViewModel';
import { ResourcePanelViewModel } from './Resource/ResourcePanelViewModel';
import { ModCategoryPanelViewModel } from './ModCategory/ModCategoryPanelViewModel';

@singleton()
export class MainPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }

    readonly appDetailPanel = new PanelViewModel(new AppDetailPanelViewModel());
    readonly resourceGroupPanel = new PanelViewModel(new ResourceGroupPanelViewModel());
    readonly resourcePanel = new PanelViewModel(new ResourcePanelViewModel());
    readonly modCategoryPanel = new PanelViewModel(new ModCategoryPanelViewModel());
}