// Generated code
import { BaseForm } from 'XtiShared/Forms/BaseForm';
import { FormComponentViewModel } from 'XtiShared/Html/FormComponentViewModel';

export class VerifyLoginForm extends BaseForm {
	constructor(vm: FormComponentViewModel = new FormComponentViewModel()) {
		super('VerifyLoginForm', vm);
		this.UserName.setCaption('User Name');
		this.UserName.constraints.mustNotBeNull();
		this.UserName.constraints.mustNotBeWhitespace('Must not be blank');
		this.UserName.setMaxLength(100);
		this.Password.setCaption('Password');
		this.Password.constraints.mustNotBeNull();
		this.Password.constraints.mustNotBeWhitespace('Must not be blank');
		this.Password.setMaxLength(100);
		this.Password.protect();
	}
	readonly UserName = this.addTextInputFormGroup('UserName');
	readonly Password = this.addTextInputFormGroup('Password');
}