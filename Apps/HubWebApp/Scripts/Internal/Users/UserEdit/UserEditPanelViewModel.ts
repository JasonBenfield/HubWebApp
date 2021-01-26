import { ComponentTemplate } from "XtiShared/ComponentTemplate";
import { ComponentViewModel } from "XtiShared/ComponentViewModel";
import { EditUserFormViewModel } from "../../../Hub/Api/EditUserFormViewModel";
import { OffscreenSubmitViewModel } from 'XtiShared/OffscreenSubmitViewModel';
import * as panelTemplate from './UserEditPanel.html';
import * as formTemplate from './EditUserForm.html';
import { AlertViewModel } from "XtiShared/Alert";
import { createCommandButtonViewModel } from "XtiShared/Templates/CommandButtonTemplate";

export class UserEditPanelViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('user-edit-panel', panelTemplate));
        new ComponentTemplate('edit-user-form', formTemplate).register();
        this.editUserForm.componentName('edit-user-form');
    }

    readonly alert = new AlertViewModel();
    readonly editUserForm = new EditUserFormViewModel();
    readonly offscreenSubmit = new OffscreenSubmitViewModel();
    readonly saveCommand = createCommandButtonViewModel();
    readonly cancelCommand = createCommandButtonViewModel();
}