// Generated code
import { BaseFormView } from '@jasonbenfield/sharedwebapp/Forms/BaseFormView';

export class EditUserFormView extends BaseFormView {
	readonly UserID = this.addHiddenFormGroup();
	readonly PersonName = this.addInputFormGroup();
	readonly Email = this.addInputFormGroup();
}