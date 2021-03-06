﻿import ko = require('knockout');
import { singleton } from 'tsyringe';
import { AlertViewModel } from 'XtiShared/Alert';
import { PageViewModel } from 'XtiShared/PageViewModel';
import { AppListItemViewModel } from './AppListItemViewModel';
import * as template from './MainPage.html';

@singleton()
export class MainPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }

    readonly alert = new AlertViewModel();
    readonly apps = ko.observableArray<AppListItemViewModel>([]);
}