// Generated code
import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { EditUserFormView } from './EditUserFormView';

export class EditUserForm extends BaseForm {
	constructor(protected readonly view: EditUserFormView) {
		super('EditUserForm', view);
		this.PersonName.setCaption('Person Name');
		this.Email.setCaption('Email');
	}
	get UserID() { return this.addHiddenNumber('UserID', this.view.UserID); };
	get PersonName() { return this.addTextInputFormGroup('PersonName', this.view.PersonName); };
	get Email() { return this.addTextInputFormGroup('Email', this.view.Email); };
}