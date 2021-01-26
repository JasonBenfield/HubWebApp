// Generated code
import { EditUserFormViewModel } from "./EditUserFormViewModel";
import { Form } from 'XtiShared/Forms/Form';

export class EditUserForm extends Form {
	constructor(private readonly vm: EditUserFormViewModel) {
		super('EditUserForm');
		this.UserID.setCaption('User ID');
		this.UserID.constraints.mustNotBeNull();
		this.PersonName.setCaption('Person Name');
		this.Email.setCaption('Email');
	}
	readonly UserID = this.addHiddenNumberField('UserID', this.vm.UserID);
	readonly PersonName = this.addTextInputField('PersonName', this.vm.PersonName);
	readonly Email = this.addTextInputField('Email', this.vm.Email);
}