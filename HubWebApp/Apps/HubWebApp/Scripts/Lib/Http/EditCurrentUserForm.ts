// Generated code
import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { BaseForm } from '@jasonbenfield/sharedwebapp/Forms/BaseForm';
import { EditCurrentUserFormView } from './EditCurrentUserFormView';

export class EditCurrentUserForm extends BaseForm {
	protected readonly view: EditCurrentUserFormView;
	
	constructor(view: EditCurrentUserFormView) {
		super('EditCurrentUserForm', view);
		this.PersonName.setCaption('Person Name');
		this.Email.setCaption('Email');
	}
	readonly PersonName = this.addTextInputFormGroup('PersonName', this.view.PersonName);
	readonly Email = this.addTextInputFormGroup('Email', this.view.Email);
}