// Generated code
import { BaseForm } from 'XtiShared/Forms/BaseForm';
import { FormComponentViewModel } from 'XtiShared/Html/FormComponentViewModel';

export class EditUserForm extends BaseForm {
	constructor(vm: FormComponentViewModel = new FormComponentViewModel()) {
		super('EditUserForm', vm);
		this.UserID.setCaption('User ID');
		this.UserID.constraints.mustNotBeNull();
		this.PersonName.setCaption('Person Name');
		this.Email.setCaption('Email');
	}
	readonly UserID = this.addHiddenNumberFormGroup('UserID');
	readonly PersonName = this.addTextInputFormGroup('PersonName');
	readonly Email = this.addTextInputFormGroup('Email');
}