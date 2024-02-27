// Generated code
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { AddUserFormView } from './AddUserFormView';

export class AddUserForm extends BaseForm {
	protected readonly view: AddUserFormView;
	
	constructor(view: AddUserFormView) {
		super('AddUserForm', view);
		this.UserName.setCaption('User Name');
		this.UserName.constraints.mustNotBeNull();
		this.UserName.constraints.mustNotBeWhitespace('Must not be blank');
		this.Password.setCaption('Password');
		this.Password.constraints.mustNotBeNull();
		this.Password.constraints.mustNotBeWhitespace('Must not be blank');
		this.Password.protect();
		this.Confirm.setCaption('Confirm');
		this.Confirm.constraints.mustNotBeNull();
		this.Confirm.protect();
		this.PersonName.setCaption('Name');
		this.Email.setCaption('Email');
	}
	readonly UserName = this.addTextInputFormGroup('UserName', this.view.UserName);
	readonly Password = this.addTextInputFormGroup('Password', this.view.Password);
	readonly Confirm = this.addTextInputFormGroup('Confirm', this.view.Confirm);
	readonly PersonName = this.addTextInputFormGroup('PersonName', this.view.PersonName);
	readonly Email = this.addTextInputFormGroup('Email', this.view.Email);
}