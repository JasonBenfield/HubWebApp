// Generated code
import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { EditUserFormView } from './EditUserFormView';

export class EditUserForm extends BaseForm {
	protected readonly view: EditUserFormView;
	
	constructor(view: EditUserFormView) {
		super('EditUserForm', view);
		this.PersonName.setCaption('Person Name');
		this.Email.setCaption('Email');
	}
	readonly UserID = this.addHiddenNumber('UserID', this.view.UserID);
	readonly PersonName = this.addTextInputFormGroup('PersonName', this.view.PersonName);
	readonly Email = this.addTextInputFormGroup('Email', this.view.Email);
}