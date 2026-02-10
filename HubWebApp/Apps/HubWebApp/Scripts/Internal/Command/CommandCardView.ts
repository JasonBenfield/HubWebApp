import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { FormGroupTextView, FormGroupView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";

export class CommandCardView extends CardView {
    readonly titleTextView: BasicTextComponentView;
    readonly cardAlertView: CardAlertView;
    readonly appFormGroupView: FormGroupTextView;
    readonly versionFormGroupView: FormGroupView;
    readonly versionTextView: BasicTextComponentView;
    readonly currentVersionTextView: BasicTextComponentView;
    readonly timeStartedFormGroupView: FormGroupTextView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleTextView = this.addCardHeader().addView(TextHeading3View);
        this.titleTextView.addCssName("card-title");
        this.cardAlertView = this.addCardAlert();
        const bodyView = this.addCardBody();
        const formGroupContainerView = bodyView.addView(FormGroupContainerView);
        this.appFormGroupView = formGroupContainerView.addFormGroupTextView();
        this.appFormGroupView.valueTextView.styleAsUserSelectAll();
        this.versionFormGroupView = formGroupContainerView.addFormGroup(FormGroupView);
        this.versionFormGroupView.caption.setText("Version");
        const versionContainerView = this.versionFormGroupView.valueCell.addView(BlockView);
        versionContainerView.styleAsFormControl();
        this.versionTextView = versionContainerView.addView(TextSpanView);
        this.versionTextView.setMargin(MarginCss.end(1));
        this.versionTextView.styleAsUserSelectAll();
        this.currentVersionTextView = versionContainerView.addView(TextSpanView);
        this.timeStartedFormGroupView = formGroupContainerView.addFormGroupTextView();
    }
}