// Generated code
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { EditUserFormView } from './EditUserFormView';

export class EditUserForm extends BaseForm {
	protected readonly view: EditUserFormView;
	
	constructor(view: EditUserFormView) {
		super('EditUserForm', view);
		this.UserID.setCaption('User ID');
		this.UserID.constraints.mustNotBeNull();
		this.PersonName.setCaption('Person Name');
		this.Email.setCaption('Email');
	}
	readonly UserID = this.addHiddenNumberFormGroup('UserID', this.view.UserID);
	readonly PersonName = this.addTextInputFormGroup('PersonName', this.view.PersonName);
	readonly Email = this.addTextInputFormGroup('Email', this.view.Email);
}