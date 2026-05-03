// Generated code
import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { AddUserFormView } from './AddUserFormView';

export class AddUserForm extends BaseForm {
	constructor(protected readonly view: AddUserFormView) {
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
	get UserName() { return this.addTextInputFormGroup('UserName', this.view.UserName); };
	get Password() { return this.addTextInputFormGroup('Password', this.view.Password); };
	get Confirm() { return this.addTextInputFormGroup('Confirm', this.view.Confirm); };
	get PersonName() { return this.addTextInputFormGroup('PersonName', this.view.PersonName); };
	get Email() { return this.addTextInputFormGroup('Email', this.view.Email); };
}