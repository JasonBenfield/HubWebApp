// Generated code
import * as ko from 'knockout';
import { InputFieldViewModel } from "XtiShared/Forms/InputFieldViewModel";
import { HiddenFieldViewModel } from "XtiShared/Forms/HiddenFieldViewModel";

export class EditUserFormViewModel {
	readonly componentName = ko.observable('EditUserForm');
	readonly UserID = new HiddenFieldViewModel();
	readonly PersonName = new InputFieldViewModel();
	readonly Email = new InputFieldViewModel();
}
