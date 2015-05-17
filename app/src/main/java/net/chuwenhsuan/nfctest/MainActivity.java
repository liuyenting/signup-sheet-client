package net.chuwenhsuan.nfctest;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.nfc.NfcAdapter;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.os.Vibrator;
import android.support.v7.app.ActionBarActivity;
import android.util.Base64;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class MainActivity extends ActionBarActivity {

    private class UserInfo {
        public String name = "";
        public String avatar = "";

        public UserInfo(String _name, String _avatar) {
            name = _name;
            avatar = _avatar;
        }
    }

    private NfcAdapter mNfcAdapter;
    static boolean submitted = false;
    static String ip_input = "";
    static String msg = "";

    final Handler handler = new Handler(){
        @Override
        public void handleMessage(Message msg) {
            final RelativeLayout r2 = (RelativeLayout)findViewById(R.id.screen2);
            final TextView name = (TextView)findViewById(R.id.name);
            final ImageView avy = (ImageView)findViewById(R.id.imageView2);
            name.setVisibility(View.VISIBLE);
            avy.setVisibility(View.VISIBLE);
            r2.setVisibility(View.VISIBLE);

            UserInfo info = (UserInfo)msg.obj;
            if (info.name.equals("")) {
                r2.setVisibility(View.VISIBLE);
                name.setText("Invalid ID");
            }
            else {
                name.setText(info.name);
                byte[] decodedString = Base64.decode(info.avatar, Base64.DEFAULT);
                Bitmap decodedByte = BitmapFactory.decodeByteArray(decodedString, 0, decodedString.length);
                avy.setImageBitmap(decodedByte);
            }

            //delay for 2.5 seconds
            Handler handler = new Handler();
            handler.postDelayed(new Runnable() {
                @Override
                public void run() {
                    name.setVisibility(View.GONE);
                    avy.setVisibility(View.GONE);
                    r2.setVisibility(View.GONE);
                }
            }, 2500);

            super.handleMessage(msg);
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //hide status and action bar
        View decorView = getWindow().getDecorView();

        int uiOptions = View.SYSTEM_UI_FLAG_FULLSCREEN;
        decorView.setSystemUiVisibility(uiOptions);
        getSupportActionBar().hide();
        setContentView(R.layout.activity_main);

        //initial setup
        mNfcAdapter = NfcAdapter.getDefaultAdapter(this);
        FrameLayout screen = (FrameLayout) findViewById(R.id.screen);
        final EditText server_ip = (EditText) findViewById(R.id.server_ip_field);
        final Button submit = (Button) findViewById(R.id.confirm_button);
        final TextView name = (TextView)findViewById(R.id.name);
        final ImageView avy = (ImageView)findViewById(R.id.imageView2);
        final RelativeLayout r2 = (RelativeLayout)findViewById(R.id.screen2);

        //hide unused elements
        name.setVisibility(View.GONE);
        avy.setVisibility(View.GONE);
        server_ip.setVisibility(View.GONE);
        submit.setVisibility(View.GONE);
        r2.setVisibility(View.GONE);

        //read from preference file
        SharedPreferences settings = getSharedPreferences("gis_demo", 0);
        String last_ip_input = settings.getString("gis_ip", "Server IP");
        server_ip.setText(last_ip_input);

        if (mNfcAdapter == null) {
            Toast.makeText(this, "This device doesn't support NFC.", Toast.LENGTH_LONG).show();
            finish();
            return;
        }

        if (!mNfcAdapter.isEnabled()) {
            Toast.makeText(this, "Please enable NFC.", Toast.LENGTH_LONG).show();
        }

        //if user taps the submit button
        submit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ip_input = server_ip.getText().toString();
                submitted = true;
                server_ip.setVisibility(View.GONE);
                submit.setVisibility(View.GONE);
                InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
                imm.hideSoftInputFromWindow(v.getWindowToken(), 0);
            }
        });

        //if user taps anywhere on the screen
        screen.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                server_ip.setVisibility(View.VISIBLE);
                submit.setVisibility(View.VISIBLE);
                server_ip.requestFocus();
                InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
                imm.toggleSoftInput(InputMethodManager.SHOW_FORCED, 0);
            }
        });

        handleIntent(getIntent());
    }

    private void handleIntent(Intent intent) {
        //check if it detects an NFC tag
        if (getIntent().getAction().equals(NfcAdapter.ACTION_TAG_DISCOVERED)) {
            if (submitted) {
                //vibrate and popup message
                Vibrator v = (Vibrator) this.getSystemService(Context.VIBRATOR_SERVICE);
                v.vibrate(100);
                msg = this.ByteArrayToHexString(getIntent().getByteArrayExtra(NfcAdapter.EXTRA_ID));
                Toast.makeText(MainActivity.this, "NFC ID detected: " + msg + ".", Toast.LENGTH_LONG).show();

                //connect to server and display results
                TransmitData transmitter = new TransmitData();

                // transmit first
                transmitter.execute();

                SharedPreferences settings = getSharedPreferences("gis_demo", 0);
                SharedPreferences.Editor editor = settings.edit();
                editor.putString("gis_ip", ip_input);

                editor.commit();


            }
            else {
                Toast.makeText(this, "Please enter the IP first. ", Toast.LENGTH_LONG).show();
            }
        }
    }

    private String ByteArrayToHexString(byte[] in_array) {
        int i, buffer, input;
        String[] hex = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        String out = "";

        for (i = 0; i < in_array.length; ++i) {
            input = (int) in_array[i] & 0xff;
            buffer = (input >> 4) & 0x0f;
            out += hex[buffer];
            buffer = input & 0x0f;
            out += hex[buffer];
        }
        return out;
    }

    private class TransmitData extends AsyncTask<String, Void, String> {

        protected String doInBackground(String... args) {

            String url = "http://" + ip_input;
            HttpClient httpclient = new DefaultHttpClient();
            HttpPost request = new HttpPost(url);

            try {
                List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
                nameValuePairs.add(new BasicNameValuePair("id", msg));
                request.setEntity(new UrlEncodedFormEntity(nameValuePairs));
                HttpResponse response = httpclient.execute(request);
                JSONObject in = new JSONObject(EntityUtils.toString(response.getEntity()));
                String name = in.getString("name");
                String avatar = in.getString("avatar");


                // Call the handler
                Message msg = handler.obtainMessage();
                msg.obj = new UserInfo(name, avatar);
                handler.sendMessage(msg);
            }
            catch (IOException e) {
                e.printStackTrace();
            }
            catch (JSONException e) {
                e.printStackTrace();
            }
            return "0";
        }


    }

}