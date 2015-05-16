package net.chuwenhsuan.nfctest;

import android.content.Context;
import android.content.Intent;
import android.nfc.NfcAdapter;
import android.os.Bundle;
import android.os.Vibrator;
import android.support.v7.app.ActionBarActivity;
import android.widget.TextView;
import android.widget.Toast;


public class MainActivity extends ActionBarActivity {

    private TextView mTextView;
    private NfcAdapter mNfcAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        mTextView = (TextView) findViewById(R.id.textView_explanation);

        mNfcAdapter = NfcAdapter.getDefaultAdapter(this);

        if (mNfcAdapter == null) {
            Toast.makeText(this, "This device doesn't support NFC.", Toast.LENGTH_LONG).show();
            finish();
            return;
        }

        if (!mNfcAdapter.isEnabled()) {
            mTextView.setText("NFC is disabled.");
            Toast.makeText(this, "Please enable NFC.", Toast.LENGTH_LONG).show();
        }
        else {
            mTextView.setText(R.string.explanation);
        }

        handleIntent(getIntent());
    }

    private void handleIntent(Intent intent) {
        if (getIntent().getAction().equals(NfcAdapter.ACTION_TAG_DISCOVERED)) {
            ((TextView) findViewById(R.id.textView_explanation)).setText("ID: " + this.ByteArrayToHexString(getIntent().getByteArrayExtra(NfcAdapter.EXTRA_ID)));
            Vibrator v = (Vibrator) this.getSystemService(Context.VIBRATOR_SERVICE);
            v.vibrate(100);
            Toast.makeText(this, "NFC ID detected: " + this.ByteArrayToHexString(getIntent().getByteArrayExtra(NfcAdapter.EXTRA_ID)) + ".", Toast.LENGTH_LONG).show();
        }
    }

    private String ByteArrayToHexString(byte [] in_array) {
        int i, buffer, input;
        String [] hex = {"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F"};
        String out= "";

        for(i = 0 ; i < in_array.length ; ++i)
        {
            input = (int) in_array[i] & 0xff;
            buffer = (input >> 4) & 0x0f;
            out += hex[buffer];
            buffer = input & 0x0f;
            out += hex[buffer];
        }
        return out;
    }
}
