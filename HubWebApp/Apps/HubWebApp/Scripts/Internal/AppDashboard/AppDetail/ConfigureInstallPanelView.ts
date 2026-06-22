import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { FormView } from "@jasonbenfield/sharedwebapp/Views/FormView";
import { FormGroupInputView, FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { ModalErrorView } from "@jasonbenfield/sharedwebapp/Views/ModalError";
import { HubTheme } from "../../HubTheme";
import { ButtonContainerView } from "@jasonbenfield/sharedwebapp/Views/ButtonContainerView";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { ModalConfirmView } from "@jasonbenfield/sharedwebapp/Views/Modal";

export class ConfigureInstallPanelView extends GridView {
    readonly cardAlertView: CardAlertView;
    private readonly formView: FormView;
    readonly configurationNameFormGroupView: FormGroupTextView;
    readonly configurationNameInputFormGroupView: FormGroupInputView;
    readonly templateNameFormGroupView: FormGroupTextView;
    readonly selectTemplateButton: ButtonCommandView;
    readonly machineNameFormGroupView: FormGroupTextView;
    readonly installSequenceInputFormGroupView: FormGroupInputView;
    readonly deleteButton: ButtonCommandView;
    readonly cancelButton: ButtonCommandView;
    readonly saveButton: ButtonCommandView;
    readonly modalConfirmView: ModalConfirmView;
    readonly modalErrorView: ModalErrorView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const cardView = mainContent.addView(CardView);
        cardView.setMargin(MarginCss.bottom(3));
        const titleTextView = cardView.addCardHeader().addView(TextHeading3View);
        titleTextView.addCssName("card-title");
        titleTextView.setText("Configuration Install Template");
        this.cardAlertView = cardView.addCardAlert();
        const cardBodyView = cardView.addCardBody();
        this.formView = cardBodyView.addView(FormView);
        this.formView.addOffscreenSubmit();
        const formGroupContainerView = this.formView.addFormGroupContainer();
        this.configurationNameFormGroupView = formGroupContainerView.addFormGroupTextView();
        this.configurationNameInputFormGroupView = formGroupContainerView.addFormGroupInputView();
        this.templateNameFormGroupView = formGroupContainerView.addFormGroupTextView();
        this.templateNameFormGroupView.valueTextView.styleAsUserSelectAll();
        this.selectTemplateButton = this.templateNameFormGroupView.valueCell.addView(ButtonContainerView).addButtonCommand();
        this.selectTemplateButton.setText("Select a different template");
        this.machineNameFormGroupView = formGroupContainerView.addFormGroupTextView();
        this.machineNameFormGroupView.valueTextView.styleAsUserSelectAll();
        this.installSequenceInputFormGroupView = formGroupContainerView.addFormGroupInputView();
        const buttonContainerView = cardBodyView.addView(ButtonContainerView);
        this.deleteButton = buttonContainerView.addButtonCommand();
        this.deleteButton.useOutlineStyle(ContextualClass.danger);
        this.deleteButton.setText("Delete Install Configuration");
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.cancelButton = HubTheme.instance.commandToolbar.cancelButton(
            toolbar.addButtonCommandToEnd()
        );
        this.cancelButton.setMargin(MarginCss.end(1));
        this.saveButton = HubTheme.instance.commandToolbar.saveButton(
            toolbar.addButtonCommandToEnd()
        );
        this.modalConfirmView = this.addView(ModalConfirmView);
        this.modalErrorView = this.addView(ModalErrorView);
    }

    handleFormSubmit(action: () => void) {
        this.formView
            .onSubmit()
            .preventDefault()
            .execute(action)
            .subscribe();
    }
}