// Generated code
import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { EditCurrentUserFormView } from './EditCurrentUserFormView';

export class EditCurrentUserForm extends BaseForm {
	constructor(protected readonly view: EditCurrentUserFormView) {
		super('EditCurrentUserForm', view);
		this.PersonName.setCaption('Person Name');
		this.Email.setCaption('Email');
	}
	get PersonName() { return this.addTextInputFormGroup('PersonName', this.view.PersonName); };
	get Email() { return this.addTextInputFormGroup('Email', this.view.Email); };
}